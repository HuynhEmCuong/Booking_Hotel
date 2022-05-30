using Booking_Hotel.Application.ViewModels;
using Booking_Hotel.Data.Enums.Reservation;
using Booking_Hotel.Ultilities;
using Booking_Hotel.Utilities.Dtos;
using Booking_Hotel.Web.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PayPal.Core;
using PayPal.v1.Payments;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static Booking_Hotel.Web.TagHelpers.MessageTagHelper;

namespace Booking_Hotel.Web.Controllers
{
    public class ReservationController : Controller
    {
        #region Fields
        private readonly ILogger<ReservationController> _logger;
        private readonly IOptions<Config> _config;
        private readonly HttpClient client;
        private readonly IOptions<PaypalSetting> _paypalSetting;
        #endregion

        #region Ctor
        public ReservationController(ILogger<ReservationController> logger, IHttpClientFactory clientFactory, IOptions<Config> config, IOptions<PaypalSetting> paypalSetting)
        {
            client = clientFactory.CreateClient("default");
            _logger = logger;
            _config = config;
            _paypalSetting = paypalSetting;
        }
        #endregion
        [HttpPost]
        [Route("/dat-phong", Name = "reservation")]
        public IActionResult Index(ReservationParams model)
        {
            var entity = new ReservationFormViewModel
            {
                CheckInDate = model.CheckInDate,
                CheckOutDate = model.CheckOutDate,
                Adult = model.Adult,
                Kids = model.Kids,
                RoomCatId = model.RoomCatId
            };
            ViewBag.Data = entity;
            return View();
        }
        [HttpGet]
        [Route("/dat-phong/xac-nhan", Name = "confirmation")]
        public async Task<IActionResult> Confirmation([FromQuery] ConfirmationViewModel model)
        {
            var response = await client.GetAsync($"/api/Room/GetIsAvailableRoomByCat?catId={model.RoomCatID}");

            if (response == null)
                return View();

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return View();

            string json = response.Content.ReadAsStringAsync().Result;
            RoomViewModel data = JsonConvert.DeserializeObject<RoomViewModel>(json);
            var totalDays = (ConvertVnDateToEnDate(model.CheckInDate) - ConvertVnDateToEnDate(model.CheckOutDate)).TotalDays;
            model.Name = data.RoomCategory.Name;
            model.TotalPrice = totalDays * data.RoomCategory.Price;
            model.RoomID = data.Id;
            ViewBag.Data = model;
            return View();
        }


        [HttpGet]
        [Route("/thanh-toan", Name = "pay")]
        public IActionResult Payment([FromQuery] PaymentViewModel model)
        {
            ViewBag.Data = model;
            return View();
        }


        [Route("/thanh-toan/paypal", Name = "pay-paypal")]
        public async Task<IActionResult> PaypalCheckout(PaypalViewModel model)
        {
            var response = await client.GetAsync($"/api/Room/GetIsAvailableRoomByCat?catId={model.RoomCatID}");

            if (response == null)
                return View();

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return View();

            string json = response.Content.ReadAsStringAsync().Result;
            RoomViewModel data = JsonConvert.DeserializeObject<RoomViewModel>(json);

            var environment = new SandboxEnvironment(_paypalSetting.Value.ClientId, _paypalSetting.Value.Secret);
            var clientPaypal = new PayPalHttpClient(environment);

            #region Create Paypal Order
            var totalDays = (ConvertVnDateToEnDate(model.CheckInDate) - ConvertVnDateToEnDate(model.CheckOutDate)).TotalDays;

            var itemList = new ItemList()
            {
                Items = new List<Item>()
            };
            var total = Math.Round((data.RoomCategory.Price * totalDays) / _paypalSetting.Value.ExchangeRate, 2);

            itemList.Items.Add(new Item()
            {
                Name = data.Code,
                Currency = "USD",
                Price = Math.Round((data.RoomCategory.Price * totalDays) / _paypalSetting.Value.ExchangeRate, 2).ToString(),
                Quantity = 1.ToString(),
                Sku = "sku",
                Tax = "0"
            });
            #endregion
            var checkInDate = ConvertVnDateToEnDate(model.CheckInDate).ToString("MM-dd-yyyy");
            var checkOutDate = ConvertVnDateToEnDate(model.CheckOutDate).ToString("MM-dd-yyyy");
            model.TotalPrice = totalDays * data.RoomCategory.Price;

            string query = $"?checkInDate={checkInDate}&checkOutDat={checkOutDate}&roomCatID={model.RoomCatID}&adult={model.Adult}&kids={model.Kids}&fullName={HttpUtility.UrlEncode(model.FullName)}&email={model.Email}&phone={model.Phone}&roomID={model.RoomID}&totalPrice={model.TotalPrice}";
            var paypalOrderId = DateTime.Now.Ticks;
            var hostname = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            var payment = new Payment()
            {
                Intent = "sale",
                Transactions = new List<Transaction>()
                {
                    new Transaction()
                    {
                        Amount = new Amount()
                        {
                            Total = total.ToString(),
                            Currency = "USD",
                            Details = new AmountDetails
                            {
                                Tax = "0",
                                Shipping = "0",
                                Subtotal = total.ToString()
                            }
                        },
                        ItemList = itemList,
                        Description = $"Invoice #{paypalOrderId}",
                        InvoiceNumber = paypalOrderId.ToString()
                    }
                },
                RedirectUrls = new RedirectUrls()
                {
                    CancelUrl = $"{hostname}/thanh-toan/that-bai{query}",
                    ReturnUrl = $"{hostname}/thanh-toan/thanh-cong{query}"
                },
                Payer = new Payer()
                {
                    PaymentMethod = "paypal"
                }
            };

            PaymentCreateRequest request = new PaymentCreateRequest();
            request.RequestBody(payment);

            try
            {
                var responsePaypal = await clientPaypal.Execute(request);
                var statusCode = responsePaypal.StatusCode;
                Payment result = responsePaypal.Result<Payment>();

                var links = result.Links.GetEnumerator();
                string paypalRedirectUrl = null;
                while (links.MoveNext())
                {
                    LinkDescriptionObject lnk = links.Current;
                    if (lnk.Rel.ToLower().Trim().Equals("approval_url"))
                    {
                        //saving the payapalredirect URL to which user will be redirected for payment  
                        paypalRedirectUrl = lnk.Href;
                    }
                }

                return Redirect(paypalRedirectUrl);
            }
            catch (BraintreeHttp.HttpException httpException)
            {
                var statusCode = httpException.StatusCode;
                var debugId = httpException.Headers.GetValues("PayPal-Debug-Id").FirstOrDefault();

                //Process when Checkout with Paypal fails
                return Redirect($"/thanh-toan/that-bai{query}");
            }
        }
        [HttpGet]
        [Route("/thanh-toan/that-bai", Name = "pay-paypal-fail")]
        public async Task<IActionResult> CheckoutFail([FromQuery] PaypalViewModel model)
        {
            // Bước 1: kiểm tra xem số dt và email nếu chưa tồn tại thì thêm mới
            var responseGuest = await client.GetAsync($"/api/Guest/CheckExistByPhoneAndEmail?phone={model.Phone}&email={model.Email}");
            string jsonExistGuest = responseGuest.Content.ReadAsStringAsync().Result;
            GuestViewModel guestData = JsonConvert.DeserializeObject<GuestViewModel>(jsonExistGuest);

            var guestId = 0;
            if (guestData == null)
            {
                var guest = new GuestViewModel
                {
                    Email = model.Email,
                    Phone = model.Phone,
                    FullName = model.FullName
                };
                var jsonGuest = JsonConvert.SerializeObject(guest);
                var stringContentGuest = new StringContent(jsonGuest, UnicodeEncoding.UTF8, "application/json");

                var responseInsertGuest = await client.PostAsync("api/Guest/Add", stringContentGuest); //Gửi lên server Post async

                if (responseInsertGuest == null)
                {
                    //Hiện thông báo lỗi
                    #region Hiển thị thông báo
                    ViewBag.Type = MessageType.danger;
                    ViewBag.Content = "Đã xảy ra lỗi. Vui lòng thử lại";
                    #endregion
                    return View();
                }
                if (responseInsertGuest.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string jsonInsertGuest = responseInsertGuest.Content.ReadAsStringAsync().Result;
                    OperationResultViewModel res = JsonConvert.DeserializeObject<OperationResultViewModel>(jsonInsertGuest);
                    GuestReturnViewModel data = res.Data;
                    guestId = data.Id;
                }

            }
            else
            {
                guestId = guestData.Id;
            }

            // Bước 2: Thêm mới đơn hàng và cập nhật phương thức thanh toán là paypal
            var reservation = new ReservationViewModel
            {
                RoomId = model.RoomCatID,
                GuestId = guestId,
                CheckInDate = ConvertVnDateToEnDate(model.CheckInDate),
                CheckOutDate = ConvertVnDateToEnDate(model.CheckOutDate),
                TotalPrice = model.TotalPrice,
                PaymentType = PaymentType.Paypal,
                ReservationStatus = ReservationStatus.Cancel
            };
            var jsonReservation = JsonConvert.SerializeObject(reservation);
            var stringContentReservation = new StringContent(jsonReservation, UnicodeEncoding.UTF8, "application/json");
            var responseReservation = await client.PostAsync("api/Reservation/add", stringContentReservation); //Gửi lên server Post async

            if (responseReservation == null)
            {
                //Hiện thông báo lỗi
                #region Hiển thị thông báo
                ViewBag.Type = MessageType.danger;
                ViewBag.Content = "Đã xảy ra lỗi. Vui lòng thử lại";
                #endregion
                return View();
            }

            if (responseReservation.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //Hiện thông báo lỗi
                #region Hiển thị thông báo
                ViewBag.Type = MessageType.danger;
                ViewBag.Content = "Đã xảy ra lỗi. Vui lòng thử lại";
                #endregion
                return View();
            }
            else
            {
                //Hiện chúc mừng

                #region Hiển thị thông báo
                ViewBag.Type = MessageType.success;
                ViewBag.Content = "Xin chúc mừng bạn đã thanh toán thành công.";
                #endregion

                //Trả về kết quả
                return View();
            }
        }
        [HttpGet]
        [Route("/thanh-toan/thanh-cong", Name = "pay-paypal-success")]
        public async Task<IActionResult> CheckoutSuccess(PaypalViewModel model)
        {
            // Bước 1: kiểm tra xem số dt và email nếu chưa tồn tại thì thêm mới
            var responseGuest = await client.GetAsync($"/api/Guest/CheckExistByPhoneAndEmail?phone={model.Phone}&email={model.Email}");
            string jsonExistGuest = responseGuest.Content.ReadAsStringAsync().Result;
            GuestViewModel guestData = JsonConvert.DeserializeObject<GuestViewModel>(jsonExistGuest);

            var guestId = 0;
            if (guestData == null)
            {
                var guest = new GuestViewModel
                {
                    Email = model.Email,
                    Phone = model.Phone,
                    FullName = model.FullName
                };
                var jsonGuest = JsonConvert.SerializeObject(guest);
                var stringContentGuest = new StringContent(jsonGuest, UnicodeEncoding.UTF8, "application/json");

                var responseInsertGuest = await client.PostAsync("api/Guest/Add", stringContentGuest); //Gửi lên server Post async

                if (responseInsertGuest == null)
                {
                    //Hiện thông báo lỗi
                    #region Hiển thị thông báo
                    ViewBag.Type = MessageType.danger;
                    ViewBag.Content = "Đã xảy ra lỗi. Vui lòng thử lại";
                    #endregion
                    return View();
                }
                if (responseInsertGuest.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string jsonInsertGuest = responseInsertGuest.Content.ReadAsStringAsync().Result;
                    OperationResultViewModel res = JsonConvert.DeserializeObject<OperationResultViewModel>(jsonInsertGuest);
                    GuestReturnViewModel data = res.Data;
                    guestId = data.Id;
                }

            }
            else
            {
                guestId = guestData.Id;
            }

            // Bước 2: Thêm mới đơn hàng và cập nhật phương thức thanh toán là paypal
            var reservation = new ReservationViewModel
            {
                RoomId = model.RoomCatID,
                GuestId = guestId,
                CheckInDate = ConvertVnDateToEnDate(model.CheckInDate),
                CheckOutDate = ConvertVnDateToEnDate(model.CheckOutDate),
                TotalPrice = model.TotalPrice,
                PaymentType = PaymentType.Paypal,
                ReservationStatus = ReservationStatus.Success
            };
            var jsonReservation = JsonConvert.SerializeObject(reservation);
            var stringContentReservation = new StringContent(jsonReservation, UnicodeEncoding.UTF8, "application/json");
            var responseReservation = await client.PostAsync("api/Reservation/add", stringContentReservation); //Gửi lên server Post async

            if (responseReservation == null)
            {
                //Hiện thông báo lỗi
                #region Hiển thị thông báo
                ViewBag.Type = MessageType.danger;
                ViewBag.Content = "Đã xảy ra lỗi. Vui lòng thử lại";
                #endregion
                return View();
            }

            if (responseReservation.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //Hiện thông báo lỗi
                #region Hiển thị thông báo
                ViewBag.Type = MessageType.danger;
                ViewBag.Content = "Đã xảy ra lỗi. Vui lòng thử lại";
                #endregion
                return View();
            }
            else
            {
                //Hiện chúc mừng

                #region Hiển thị thông báo
                ViewBag.Type = MessageType.success;
                ViewBag.Content = "Xin chúc mừng bạn đã thanh toán thành công.";
                #endregion

                //Trả về kết quả
                return View();
            }

        }
        private DateTime ConvertVnDateToEnDate(string date)
        {
            if (date.IsNullOrEmpty())
            {
                return DateTime.MinValue;
            }
            else
            {
                var enmonth = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
                var vnmonth = new List<string> { "Th1", "Th2", "Th3", "Th4", "Th5", "Th6", "Th7", "Th8", "Th9", "Th10", "Th11", "Th12" };
                int i = 0;
                foreach (var item in vnmonth)
                {
                    if (date.Contains(item))
                    {
                        date = date.Replace(item, enmonth[i]);
                        break;
                    }
                    i++;
                }
                return Convert.ToDateTime(date);
            }
        }
    }
}

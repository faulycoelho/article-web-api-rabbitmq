using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class PayController(IPaymentUseCase paymentUseCase) : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProcessPayment(PaymentDTO model)
        {
            if (ModelState.IsValid)
            {
                await paymentUseCase.GenerateAsync(model);
                return RedirectToAction("PaymentSuccess");
            }
            return View("Index");
        }

        public IActionResult PaymentSuccess()
        {
            return View();
        }
    }
}

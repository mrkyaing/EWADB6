﻿using CloudHRMS.Models.ViewModels;
using CloudHRMS.Services;
using Microsoft.AspNetCore.Mvc;
//တု-တစ်ခြားသူပြုလိုက်ထားတာကို အတူယူ လိုက်ကြည့်တာ
//စု - ကောင်းတာ သင့်လျော်တာ စုဆောင်းခြင်း
//ပြု -မိမိအတွက်ကိုယ်တိုင်ပြုစုခြင်း 
/*
သု = သုဏေယျ… နာကြားရာ၏။
စိ = စိန္တေယျ.. ကြံစည်ရာ၏။
ပု = ပုစ္ဆေယျ.. မေးမြန်းရာ၏။
ဘာ = ဘာသေယျ.. ရွတ်ဖတ်ရာ၏။
ဝိ = ဝိစာရေယျ .. စူးစမ်းဆင်ခြင် စိစစ်ရာ၏။
လိ = လိခေယျ.. ရေးမှတ်ရာ၏။
သိ = သိက္ခေယျ.. သင်ကြားရာ၏။ (တစ်ပါးသူအားပြန်လည်သင်ကြားရာ၏)
ဓာ = ဓာရေယျ.. နှုတ်တက်ဆောင်ရာ၏။
 */
namespace CloudHRMS.Controllers {
    public class PositionController : Controller {
        private readonly IPositionService _positionService;
        public PositionController(IPositionService positionService) {
            this._positionService = positionService;
        }
        public IActionResult Entry() {
            return View();
        }
        [HttpPost]
        public IActionResult Entry(PositionViewModel positionViewModel) {
            try {
                _positionService.Create(positionViewModel);
                TempData["Msg"] = "Position record is created successfully.";
            }
            catch (Exception) {
                TempData["Msg"] = "Error occurs when Position record is created.";
            }
            return RedirectToAction("List");
        }
        public IActionResult List() => View(_positionService.GetAll());

        public IActionResult DeletebyId(string id) {
            try {
                _positionService.Delete(id);
                TempData["Msg"] = "Position record is deleted successfully.";
            }
            catch (Exception ex) {
                TempData["Msg"] = "Error occurs when Position record is deleted.";
            }
            return RedirectToAction("List");
        }
        public IActionResult Edit(string Id) {
            return View(_positionService.GetById(Id));
        }
        [HttpPost]
        public IActionResult Update(PositionViewModel positionViewModel) {
            try {
                _positionService.Update(positionViewModel);
                TempData["Msg"] = "Position record is updated SUCCESSFULLY.";
            }
            catch (Exception ex) {
                TempData["Msg"] = "Error occurs when Position record is updated.";
            }
            return RedirectToAction("List");
        }
    }
}
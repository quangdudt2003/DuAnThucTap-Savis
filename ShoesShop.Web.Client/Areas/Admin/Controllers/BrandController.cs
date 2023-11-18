﻿using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.BLL.Common.ViewModels;
using ShoesShop.BLL.ViewModels.Brands;
using ShoesShop.DAL.Constants;

namespace ShoesShop.Web.Client.Areas.Admin.Controllers;

[Authorize(Roles = SecurityRoles.Manager)]
public class BrandController : BaseAdminController
{
    private readonly IBrandService _brandService;

    public BrandController(IBrandService brandService)
    {
        _brandService = brandService;
    }

    // GET
    public IActionResult Index()
    {
        return View();
    }

    // GET
    public async Task<IActionResult> GetListPartial()
    {
        var data = await _brandService.GetListBrand();

        return PartialView("_BrandListPartial", data);
        /*
         * PartialView("PartialView", data)
         * Trả về PartialView và sử dụng dữ liệu truyền vào để tạo nội dung của PartialView đó
         * nội dung PartialView này sẽ được trả về client và có thể được hiển thị trên trang web mà không cần tải lại trang hoàn chỉnh.
         */
    }

    // GET
    public IActionResult AddNewPartial()
    {
        return PartialView("_AddNewPartial");
        /*
         * PartialView("PartialView")
         *  trả về một PartialView  từ một hành động (action) trong controller.
         * nội dung PartialView PartialView thường chứa mã HTML và Razor để hiển thị một phần cụ thể của giao diện người dùng và  cho phép hiển thị một phần của trang web mà không cần tải lại toàn bộ trang.
         * thường được sử dụng trong các tình huống như tạo hoặc chỉnh sửa dữ liệu, nơi bạn muốn hiển thị một form hoặc một phần giao diện người dùng để nhập thông tin
         * tạo ra trải nghiệm tương tác mượt mà cho người dùng và giúp giảm thiểu việc tải lại trang.
         */
    }

    [HttpPost]
    public async Task<IActionResult> SubmitAddNew([FromBody] AddNewBrandModel request)
    {
        var success = await _brandService.AddNew(request);

        return success ? SuccessResponse("Saved successfully") : BadRequestResponse("Saved fail");
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromBody] DeleteModel request)
    {
        var success = await _brandService.Delete(request);

        return success ? SuccessResponse("Saved successfully") : BadRequestResponse("Saved fail");
    }

    // GET
    public async Task<IActionResult> UpdatePartial(string id)
    {
        var data = await _brandService.GetDetail(id);

        return PartialView("_UpdatePartial", data);
    }

    [HttpPost]
    public async Task<IActionResult> SubmitUpdate([FromBody] UpdateBrandModel request)
    {
        var success = await _brandService.Update(request);

        return success ? SuccessResponse("Saved successfully") : BadRequestResponse("Saved fail");
    }
}
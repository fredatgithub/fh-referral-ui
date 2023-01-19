using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FamilyHubs.ReferralUi.Ui.Pages.ProfessionalReferral;

[Authorize(Policy = "Referrer")]
public class FamilyContactModel : PageModel
{
    [BindProperty]
    public string ReferralId { get; set; } = default!;

    [BindProperty]
    public string FullName { get; set; } = default!;

    [BindProperty]
    public string Id { get; set; } = default!;
    [BindProperty]
    public string Name { get; set; } = default!;

    [BindProperty]
    public bool ValidationValid { get; set; } = true;

    public void OnGet(string id, string name, string fullName, string referralId)
    {
        Id = id;
        Name = name;
        FullName = fullName;
        ReferralId = referralId;

        if (!string.IsNullOrEmpty(fullName))
            FullName = fullName;
    }

    public IActionResult OnPost()
    {
        ModelState.Remove("ReferralId");

        if (!ModelState.IsValid)
        {
            if (FullName == null || FullName.Trim().Length == 0 || FullName.Length > 255)
                ValidationValid = false;

            return Page();
        }

        return RedirectToPage("/ProfessionalReferral/ContactDetails", new
        {
            id = Id,
            name = Name,
            fullName = FullName,
            referralId = ReferralId
        });

    }
}

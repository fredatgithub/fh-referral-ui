using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FamilyHubs.ReferralUi.Ui.Pages.ProfessionalReferral
{
    [Authorize(Policy = "Referrer")]
    public class SafeguardingShutterModel : PageModel
    {
        
    }
}

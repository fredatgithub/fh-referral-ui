﻿using FamilyHubs.ReferralUi.Ui.Models.Configuration;
using FamilyHubs.ReferralUi.Ui.Models.Links;
using FamilyHubs.ReferralUi.Ui.Services;

namespace FamilyHubs.ReferralUi.Ui.Models;

public class FooterViewModel : IFooterViewModel
{
    const string BuiltByHRef = "http://gov.uk/esfa";
    const string SurveyHRef = "https://www.smartsurvey.co.uk/";
    const string OpenGovernmentLicenseHRef = "https://www.nationalarchives.gov.uk/doc/open-government-licence/version/3/";
    const string CrownCopyrightHRef = "https://www.nationalarchives.gov.uk/information-management/re-using-public-sector-information/uk-government-licensing-framework/crown-copyright/";

    public IReadOnlyList<Link> Links => _linkCollection.Links;

    public bool UseLegacyStyles { get; private set; }

    private readonly ILinkCollection _linkCollection;
    private readonly ILinkHelper _linkHelper;
    private readonly IUrlHelper _urlHelper;

    public FooterViewModel(
        IFooterConfiguration configuration,
        IUserContext userContext,
        ILinkCollection? linkCollection = null,
        ILinkHelper? linkHelper = null,
        IUrlHelper? urlHelper = null,
        bool useLegacyStyles = false)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        ArgumentNullException.ThrowIfNull(userContext);

        _linkCollection = new LinkCollection();

        _linkCollection = linkCollection ?? new LinkCollection();
        _linkHelper = linkHelper ?? new LinkHelper(_linkCollection);
        _urlHelper = urlHelper ?? new UrlHelper();
        UseLegacyStyles = useLegacyStyles;

        AddOrUpdateLink(new Help(_urlHelper.GetPath(configuration.FamilyHubsBaseUrl, "service/help"), GetLinkClass()));
        AddOrUpdateLink(new Feedback(SurveyHRef, GetLinkClass()));
        AddOrUpdateLink(new Privacy(_urlHelper.GetPath(userContext, configuration.FamilyHubsBaseUrl, "privacy", "service"), GetLinkClass()));
        if (userContext?.HashedAccountId == null)
        {
            AddOrUpdateLink(new Cookies(_urlHelper.GetPath(configuration.FamilyHubsBaseUrl, "cookieConsent"), GetLinkClass()));
        }
        else
        {
            AddOrUpdateLink(new Cookies(_urlHelper.GetPath(userContext, configuration.FamilyHubsBaseUrl, "cookieConsent"), GetLinkClass()));
        }
        AddOrUpdateLink(new TermsAndConditions(_urlHelper.GetPath(configuration.FamilyHubsBaseUrl, "service/termsAndConditions/overview"), GetLinkClass()));
        AddOrUpdateLink(new BuiltBy(BuiltByHRef, GetLinkClass()));
        AddOrUpdateLink(new OpenGovernmentLicense(OpenGovernmentLicenseHRef, GetLinkClass()));
        AddOrUpdateLink(new OpenGovernmentLicenseV3(OpenGovernmentLicenseHRef, GetLinkClass()));
        AddOrUpdateLink(new CrownCopyright(CrownCopyrightHRef, UseLegacyStyles ? "" : "govuk-footer__link govuk-footer__copyright-logo"));
    }

    private string GetLinkClass()
    {
        return UseLegacyStyles ? "" : "govuk-footer__link";
    }

    public void AddOrUpdateLink<T>(T link) where T : Link
    {
        _linkCollection.AddOrUpdateLink(link);
    }

    public void RemoveLink<T>() where T : Link
    {
        _linkCollection.RemoveLink<T>();
    }

    public string RenderListItemLink<T>(bool isSelected = false, string @class = "") where T : Link
    {
        return _linkHelper.RenderListItemLink<T>(isSelected, @class);
    }

    public string RenderLink<T>(Func<string>? before = null, Func<string>? after = null, bool isSelected = false) where T : Link
    {
#pragma warning disable CS8604 // Possible null reference argument.
        return _linkHelper.RenderLink<T>(before, after, isSelected);
#pragma warning restore CS8604 // Possible null reference argument.
    }
}

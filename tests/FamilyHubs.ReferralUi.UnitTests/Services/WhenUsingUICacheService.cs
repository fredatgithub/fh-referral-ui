﻿using FamilyHubs.ReferralUi.Ui.Services.Api;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FluentAssertions;
using Newtonsoft.Json;

namespace FamilyHubs.ReferralUi.UnitTests.Services;

public class WhenUsingUICacheService : BaseClientService
{
    [Fact]
    public async Task ThenGetUICacheById()
    {
        //Arrange
        var uiCache = new UICacheDto("Id","Value");
        var json = JsonConvert.SerializeObject(uiCache);
        var mockClient = GetMockClient(json);
        UICacheService uiCacheService = new(mockClient);

        //Act
        var result = await uiCacheService.GetUICacheById("Id");

        //Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(uiCache);
    }

    [Fact]
    public async Task ThenCreateUICache()
    {
        //Arrange
        var uiCache = new UICacheDto("Id","Value");
        var mockClient = GetMockClient(uiCache.Id);
        UICacheService uiCacheService = new(mockClient);

        //Act
        var result = await uiCacheService.CreateUICache(uiCache);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(uiCache.Id);
    }

    [Fact]
    public async Task ThenUpdateUICache()
    {
        //Arrange
        var uiCache = new UICacheDto("Id","Value");
        var mockClient = GetMockClient(uiCache.Id);
        UICacheService uiCacheService = new(mockClient);

        //Act
        var result = await uiCacheService.UpdateUICache(uiCache);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(uiCache.Id);
    }
}

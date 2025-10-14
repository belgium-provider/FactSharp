using System;
using System.Net.Http;
using FactSharp.Client;
using FactSharp.Client.Abstract;
using FactSharp.Options;
using Microsoft.Extensions.DependencyInjection;

namespace FactSharp;

/// <summary>
/// Initial class for initing singleton global wefact class.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Main singleton method to init the wefact api service.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="wefactOptionsDelegate"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static IServiceCollection AddWeFactApi(this IServiceCollection services, Action<WeFactOptions> wefactOptionsDelegate)
    {
        WeFactOptions wefactOptions = new();
        wefactOptionsDelegate.Invoke(wefactOptions);

        if (string.IsNullOrWhiteSpace(wefactOptions.ApiKey))
            throw new ArgumentException("WeFact API key must be provided.", nameof(wefactOptions.ApiKey));

        services.AddSingleton(wefactOptions);
        
        //registering services.
        RegisterWeFactClient<IInvoiceClient, InvoiceClient>(services);
        
        return services;
    }
    
    /// <summary>
    /// Register client
    /// </summary>
    /// <param name="services"></param>
    /// <typeparam name="TInterface"></typeparam>
    /// <typeparam name="TImplementation"></typeparam>
    private static void RegisterWeFactClient<TInterface, TImplementation>(IServiceCollection services) where TInterface : class where TImplementation : class, TInterface
    {
        //configure client builder.

        //add a retry policy using Polly ?
    }
}
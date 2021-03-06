﻿using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using WebApplication1.Resources;

namespace WebApplication1
{
    public class CustomLocalizer : StringLocalizer<Common>
    {
        private readonly IStringLocalizer _internalLocalizer;

        public CustomLocalizer(IStringLocalizerFactory factory, IHttpContextAccessor httpContextAccessor) : base(factory)
        {
            CurrentLanguage = httpContextAccessor.HttpContext.GetRouteValue("lang") as string;
            if(string.IsNullOrEmpty(CurrentLanguage))
            {
                CurrentLanguage = "en";
            }
            if (CurrentLanguage == "ee")
            {
                CurrentLanguage = "et";
            }

            _internalLocalizer = WithCulture(new CultureInfo(CurrentLanguage));
        }

        public override LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                return _internalLocalizer[name, arguments];
            }
        }

        public override LocalizedString this[string name]
        {
            get
            {
                return _internalLocalizer[name];
            }
        }

        public string CurrentLanguage { get; set; }
    }
}

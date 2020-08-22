using System;
using System.Reflection;
using System.Resources;
using KIDS.MOBILE.APP.PARENTS.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KIDS.MOBILE.APP.PARENTS.MarkupExtensions
{
    [ContentProperty(nameof(Text))]
    public class Translate : IMarkupExtension
    {
        private static readonly string _resourceId = typeof(Resource).FullName;

        private static readonly Lazy<ResourceManager> _resourceManager =
            new Lazy<ResourceManager>(() =>
                new ResourceManager(_resourceId, typeof(Translate)
                    .GetTypeInfo().Assembly));

        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return Text == null ? null : _resourceManager.Value.GetString(Text.ToUpper());
        }
    }
}
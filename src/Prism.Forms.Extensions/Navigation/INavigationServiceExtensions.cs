using Prism.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Prism.Navigation
{
    /// <summary>
    /// A collection of extensions for the <see cref="INavigationService" />
    /// </summary>
    public static class INavigationServiceExtensions
    {
        /// <summary>
        /// Navigates to the most recent entry in the back navigation history by popping the calling Page off the navigation stack.
        /// </summary>
        /// <param name="navigationService">The Navigation Service instance</param>
        /// <param name="key">The key for the Navigation Parameter</param>
        /// <param name="param">The parameter we want to pass</param>
        /// <param name="useModalNavigation">If <c>true</c> uses PopModalAsync, if <c>false</c> uses PopAsync</param>
        /// <param name="animated">If <c>true</c> the transition is animated, if <c>false</c> there is no animation on transition.</param>
        public static Task GoBackAsync(this INavigationService navigationService, string key, object param, bool? useModalNavigation = null, bool animated = true) =>
            navigationService.GoBackAsync(new NavigationParameters { { key, param } }, useModalNavigation, animated);

        /// <summary>
        /// Navigates to the most recent entry in the back navigation history by popping the calling Page off the navigation stack.
        /// </summary>
        /// <param name="navigationService">The Navigation Service instance</param>
        /// <param name="key">The key for the Navigation Parameter</param>
        /// <param name="navigationParameters">The parameters we want to pass using the specified key</param>
        public static Task GoBackAsync(this INavigationService navigationService, string key, params object[] navigationParameters) =>
            navigationService.GoBackAsync(GetNavigationParameters(key, navigationParameters));

        /// <summary>
        /// Navigates to the most recent entry in the back navigation history by popping the calling Page off the navigation stack.
        /// </summary>
        /// <param name="navigationService">The Navigation Service instance</param>
        /// <param name="key">The key for the Navigation Parameter</param>
        /// <param name="navigationParameters">The parameters we want to pass using the specified key</param>
        /// <param name="useModalNavigation">If <c>true</c> uses PopModalAsync, if <c>false</c> uses PopAsync</param>
        /// <param name="animated">If <c>true</c> the transition is animated, if <c>false</c> there is no animation on transition.</param>
        public static Task GoBackAsync(this INavigationService navigationService, string key, IEnumerable<object> navigationParameters, bool? useModalNavigation = null, bool animated = true) =>
            navigationService.GoBackAsync(GetNavigationParameters(key, navigationParameters), useModalNavigation, animated);

        /// <summary>
        /// Initiates navigation to the target specified by the <paramref name="name"/>.
        /// </summary>
        /// <param name="navigationService">The Navigation Service</param>
        /// <param name="name">The name of the target to navigate to.</param>
        /// <param name="key">The navigation key to use</param>
        /// <param name="param">The parameter to pass</param>
        /// <param name="useModalNavigation">If <c>true</c> uses PopModalAsync, if <c>false</c> uses PopAsync</param>
        /// <param name="animated">If <c>true</c> the transition is animated, if <c>false</c> there is no animation on transition.</param>
        public static Task NavigateAsync(this INavigationService navigationService, string name, string key, object param, bool? useModalNavigation = null, bool animated = true) =>
            navigationService.NavigateAsync(name, new NavigationParameters { { key, param } }, useModalNavigation, animated);

        /// <summary>
        /// Initiates navigation to the target specified by the <paramref name="name"/>.
        /// </summary>
        /// <param name="navigationService">The Navigation Service</param>
        /// <param name="name">The name of the target to navigate to.</param>
        /// <param name="key">The navigation key to use</param>
        /// <param name="navigationParameters">The parameters to pass</param>
        public static Task NavigateAsync(this INavigationService navigationService, string name, string key, params object[] navigationParameters) =>
            navigationService.NavigateAsync(name, GetNavigationParameters(key, navigationParameters));

        /// <summary>
        /// Initiates navigation to the target specified by the <paramref name="name"/>.
        /// </summary>
        /// <param name="navigationService">The Navigation Service</param>
        /// <param name="name">The name of the target to navigate to.</param>
        /// <param name="key">The navigation key to use</param>
        /// <param name="navigationParameters">The parameters to pass</param>
        /// <param name="useModalNavigation">If <c>true</c> uses PopModalAsync, if <c>false</c> uses PopAsync</param>
        /// <param name="animated">If <c>true</c> the transition is animated, if <c>false</c> there is no animation on transition.</param>
        public static Task NavigateAsync(this INavigationService navigationService, string name, string key, IEnumerable<object> navigationParameters, bool? useModalNavigation = null, bool animated = true) =>
            navigationService.NavigateAsync(name, GetNavigationParameters(key, navigationParameters), useModalNavigation, animated);

        /// <summary>
        /// Initiates navigation to the target specified by the <paramref name="name"/>.
        /// </summary>
        /// <param name="navigationService">The Navigation Service</param>
        /// <param name="name">The name of the target to navigate to.</param>
        /// <param name="param">The parameter to pass. The key will be generated as the camelCase name of the Type</param>
        /// <param name="useModalNavigation">If <c>true</c> uses PopModalAsync, if <c>false</c> uses PopAsync</param>
        /// <param name="animated">If <c>true</c> the transition is animated, if <c>false</c> there is no animation on transition.</param>
        public static Task NavigateAsync<T>(this INavigationService navigationService, string name, T param, bool? useModalNavigation = null, bool animated = true) =>
            navigationService.NavigateAsync(name, new NavigationParameters { { typeof(T).Name.Camelize(), param } }, useModalNavigation, animated);

        /// <summary>
        /// Initiates navigation to the target specified by the <paramref name="name"/>.
        /// </summary>
        /// <param name="navigationService">The Navigation Service</param>
        /// <param name="name">The name of the target to navigate to.</param>
        /// <param name="navigationParameters">The parameters to pass. The key will be generated as the camelized version of the Type being passed</param>
        /// <param name="useModalNavigation">If <c>true</c> uses PopModalAsync, if <c>false</c> uses PopAsync</param>
        /// <param name="animated">If <c>true</c> the transition is animated, if <c>false</c> there is no animation on transition.</param>
        public static Task NavigateAsync<T>(this INavigationService navigationService, string name, IEnumerable<T> navigationParameters, bool? useModalNavigation = null, bool animated = true) where T : class =>
            navigationService.NavigateAsync(name, GetNavigationParameters<T>(navigationParameters), useModalNavigation, animated);

        /// <summary>
        /// Will remove the last View from either the modal or non-modal stack
        /// </summary>
        /// <param name="navigationService"></param>
        /// <param name="name"></param>
        /// <param name="parameters"></param>
        public static async Task RemoveViewAsync(this INavigationService navigationService, string name, NavigationParameters parameters = null)
        {
            if(parameters == null)
            {
                parameters = new NavigationParameters
                {
                    { KnownNavigationParameters.NavigationMode, NavigationMode.Back }
                };
            }
            var formsNav = GetFormsNavigationService(navigationService);
            var pageType = PageNavigationRegistry.GetPageType(name);

            var page = formsNav.ModalStack.LastOrDefault(p => p.GetType() == pageType);
            if(page == null)
            {
                page = formsNav.NavigationStack.LastOrDefault(p => p.GetType() == pageType);
            }

            if (page != null && PageUtilities.CanNavigate(page, parameters) && await PageUtilities.CanNavigateAsync(page, parameters))
            {
                formsNav.RemovePage(page);
                PageUtilities.OnNavigatedFrom(page, parameters);
                PageUtilities.DestroyPage(page);
            }
        }

        private static INavigation GetFormsNavigationService(INavigationService navigationService)
        {
            var page = (IPageAware)navigationService;
            return page.Page.Navigation;
        }

        private static NavigationParameters GetNavigationParameters<T>(IEnumerable<T> navigationParameters) where T : class
        {
            var key = typeof(T).Name.Camelize();
            return GetNavigationParameters(key, navigationParameters);
        }

        private static NavigationParameters GetNavigationParameters(string key, IEnumerable<object> navigationParameters)
        {
            var parameters = new NavigationParameters();
            foreach(var item in navigationParameters)
            {
                parameters.Add(key, item);
            }
            return parameters;
        }

        private static string Camelize(this string str) =>
            char.ToLowerInvariant(str[0]) + str.Substring(1);
    }
}
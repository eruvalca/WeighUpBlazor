using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeighUpBlazor.Client.Shared
{
    public partial class LoginDisplay
    {
        [Inject]
        private NavigationManager Navigation { get; set; }
        [Inject]
        private SignOutSessionStateManager SignOutManager { get; set; }

        private async Task BeginLogout(MouseEventArgs args)
        {
            await SignOutManager.SetSignOutState();
            Navigation.NavigateTo("authentication/logout");
        }
    }
}

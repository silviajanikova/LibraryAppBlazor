using LibraryAppBlazor.Models;
using Microsoft.AspNetCore.Components;

namespace LibraryAppBlazor.Components.Pages.Members
{
    public partial class MemberForm
    {
        public Member MemberData { get; set; } = new();

        [Parameter]
        public string MemberId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            if (MemberId != null)
            {
                MemberData = memberRepository.GetById(Guid.Parse(MemberId));

                if (MemberData == null)
                {
                    NavManager.NavigateTo("/Error");
                }
            }

            await Task.CompletedTask;
        }

        private async Task Submit()
        {
            // add member
            if (MemberId == null)
            {
                MemberData.Id = Guid.NewGuid();
                memberRepository.Add(MemberData);

                NavManager.NavigateTo("/Members");
            }

            // edit existing book
            else
            {
                memberRepository.Edit(MemberData);

                NavManager.NavigateTo("/Members");
            }
        }
    }
}

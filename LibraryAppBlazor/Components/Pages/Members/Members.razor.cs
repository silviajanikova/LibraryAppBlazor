using LibraryAppBlazor.Models;
using LibraryAppBlazor.Repository;
using Microsoft.AspNetCore.Components;

namespace LibraryAppBlazor.Components.Pages.Members
{
    public partial class Members
    {
        [Inject]
        IRepository<Member> memberRepository { get; set; }

        public IEnumerable<Member> MemberList { get; set; } = new List<Member>();

        protected override void OnInitialized()
        {
            MemberList = memberRepository.List();
        }

        public void DeleteMember(Guid id)
        {
            var member = MemberList.Single(b => b.Id == id);
            memberRepository.Delete(member);

            NavManager.NavigateTo("/Members");

            MemberList = memberRepository.List();
        }
    }
}

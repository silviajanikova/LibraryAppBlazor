using LibraryAppBlazor.Models;

namespace LibraryAppBlazor.Components.Pages.Members
{
    public partial class Members
    {
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

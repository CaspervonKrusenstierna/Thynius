import "GroupsViewPage.css"
import GroupsViewHeader from "./components/groupsviewheader/GroupsViewHeader";
import GroupsViewContainer from "./components/groupsviewcontainer/GroupsViewContainer";
import useGroupsInfo from "./hooks/useGroupsInfo";


function GroupsViewPage() {
    const groupsInfo = useGroupsInfo();

    return (
        <div className="Page">
            <GroupsViewHeader></GroupsViewHeader>
            <GroupsViewContainer></GroupsViewContainer>
        </div>
    );
}

export default GroupsViewPage;
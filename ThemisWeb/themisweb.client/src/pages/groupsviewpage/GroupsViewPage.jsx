
import "../../shared/styles/Page.css"
import DashboardHeader from "../../shared/components/dashboardheader/DashboardHeader";
import GroupsView from "./components/groupsview/GroupsView";

function GroupsViewPage() {
    return (
        <div className='Page-Container'>
        <DashboardHeader></DashboardHeader>
        <GroupsView></GroupsView>
      </div>
    );
}

export default GroupsViewPage;
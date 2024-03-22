
import "../../shared/styles/Page.css"
import DashboardHeader from "../../shared/components/dashboardheader/DashboardHeader";
import ProfilePageView from "./profilepageview/ProfilePageView";

const ProfilePage = () => {
  return (
    <div className='Page-Container'>
        <DashboardHeader></DashboardHeader>
        <ProfilePageView></ProfilePageView>
    </div>
  )
}

export default ProfilePage
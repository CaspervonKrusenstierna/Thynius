import React from 'react'
import "../../shared/styles/Page.css"
import GroupPageView from './components/grouppageview/GroupPageView'
import DashboardHeader from '../../shared/components/dashboardheader/DashboardHeader'

const GroupPage = () => {
    return (
      <div className='Page-Container'>
        <DashboardHeader></DashboardHeader>
        <GroupPageView></GroupPageView>
      </div>
  );
}

export default GroupPage
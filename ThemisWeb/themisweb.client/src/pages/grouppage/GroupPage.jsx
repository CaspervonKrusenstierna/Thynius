import React from 'react'
import GroupPageView from './components/grouppageview/GroupPageView'
import DashboardHeader from '../../shared/components/dashboardheader/DashboardHeader'

const GroupPage = () => {
    return (
      <>
        <DashboardHeader></DashboardHeader>
        <GroupPageView></GroupPageView>
      </>
  );
}

export default GroupPage
import React from 'react'
import DashboardHeader from '../../shared/components/dashboardheader/DashboardHeader'
import GroupPageView from './grouppageview/GroupPageView';

const GroupPage = () => {
    return (
      <>
        <DashboardHeader></DashboardHeader>
        <GroupPageView></GroupPageView>   
      </>
  );
}

export default GroupPage
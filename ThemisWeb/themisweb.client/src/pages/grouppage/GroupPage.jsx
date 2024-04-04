import React from 'react'
import GroupPageView from './grouppageview/GroupPageView';
import { DashboardHeader } from '../../shared/components/dashboard';

const GroupPage = () => {
    return (
      <>
        <DashboardHeader className></DashboardHeader>
        <GroupPageView></GroupPageView>   
      </>
  );
}

export default GroupPage
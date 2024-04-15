import React from 'react'
import GroupPageView from './grouppageview/GroupPageView';
import { DashboardHeader } from '../../shared/components/dashboard';
import AssureLoggedIn from '../../shared/hooks/useAssureLoggedIn';

const GroupPage = () => {
  AssureLoggedIn();
    return (
      <>
        <DashboardHeader className></DashboardHeader>
        <GroupPageView></GroupPageView>   
      </>
  );
}

export default GroupPage
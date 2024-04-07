import React from 'react'
import GroupPageView from './grouppageview/GroupPageView';
import { DashboardHeader } from '../../shared/components/dashboard';
import BottomRightContainerButton from '../../shared/components/dashboard/bottomrightcontainerbutton/BottomRightContainerButton';

const GroupPage = () => {
    return (
      <>
        <DashboardHeader className></DashboardHeader>
        <GroupPageView></GroupPageView>   
      </>
  );
}

export default GroupPage
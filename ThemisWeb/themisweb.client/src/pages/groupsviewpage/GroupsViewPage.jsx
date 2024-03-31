import React from 'react'
import DashboardHeader from '../../shared/components/dashboardheader/DashboardHeader'
import GroupsView from './components/groupsview/GroupsView'

const GroupsViewPage = () => {
  return (
    <>
    <DashboardHeader></DashboardHeader>
    <div className='flex flex-column justify-center'>
      <GroupsView></GroupsView>
    </div>    
    </>
  )
}

export default GroupsViewPage
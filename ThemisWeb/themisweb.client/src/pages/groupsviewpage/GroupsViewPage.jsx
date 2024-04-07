import React from 'react'
import GroupsView from './components/groupsview/GroupsView'
import { DashboardHeader } from '../../shared/components/dashboard'

const GroupsViewPage = () => {
  return (
    <>
    <DashboardHeader></DashboardHeader>
    <div className='flex flex-column justify-center w-[100vw]'>
      <GroupsView></GroupsView>
    </div>    
    </>
  )
}

export default GroupsViewPage
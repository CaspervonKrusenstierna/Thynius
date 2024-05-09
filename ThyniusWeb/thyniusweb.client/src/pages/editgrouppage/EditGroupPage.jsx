import React from 'react'
import { DashboardHeader } from '../../shared/components/dashboard'
import EditGroupView from './creategroupview/EditGroupView'
import AssureLoggedIn from '../../shared/hooks/useAssureLoggedIn'

const EditGroupPage = () => {
  AssureLoggedIn();
  return (
    <>
      <DashboardHeader></DashboardHeader>
      <EditGroupView></EditGroupView>
    </>
  )
}

export default EditGroupPage
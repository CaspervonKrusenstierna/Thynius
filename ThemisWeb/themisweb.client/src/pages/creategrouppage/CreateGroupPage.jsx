import React from 'react'
import CreateGroupView from './creategroupview/CreateGroupView'
import { DashboardHeader } from '../../shared/components/dashboard'
import AssureLoggedIn from '../../shared/hooks/useAssureLoggedIn'

const CreateGroupPage = () => {
  AssureLoggedIn();
  return (
  <>
    <DashboardHeader></DashboardHeader>
    <CreateGroupView></CreateGroupView>
  </>
  )
}

export default CreateGroupPage
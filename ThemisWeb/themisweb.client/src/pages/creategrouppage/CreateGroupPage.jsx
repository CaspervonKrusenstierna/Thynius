import React from 'react'
import CreateGroupView from './creategroupview/CreateGroupView'
import { DashboardHeader } from '../../shared/components/dashboard'

const CreateGroupPage = () => {
  return (
  <>
    <DashboardHeader></DashboardHeader>
    <CreateGroupView></CreateGroupView>
  </>
  )
}

export default CreateGroupPage
import React from 'react'
import "../../shared/styles/Page.css"
import DashboardHeader from '../../shared/components/dashboardheader/DashboardHeader'
import CreateGroupView from './creategroupview/CreateGroupView'

const CreateGroupPage = () => {
  return (
  <div className='Page-Container'>
    <DashboardHeader></DashboardHeader>
    <CreateGroupView></CreateGroupView>
  </div>
  )
}

export default CreateGroupPage
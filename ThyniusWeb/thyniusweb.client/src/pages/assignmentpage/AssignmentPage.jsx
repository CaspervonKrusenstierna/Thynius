import React from 'react'
import AssignmentPageView from './assignmentpageview/AssignmentPageView'
import { DashboardHeader } from '../../shared/components/dashboard'
import AssureLoggedIn from '../../shared/hooks/useAssureLoggedIn'

const AssignmentPage = () => {
  AssureLoggedIn();
  return (
    <>
    <DashboardHeader></DashboardHeader>
    <div style={{display: "flex", justifyContent: "center", paddingBottom:"5px"}}>
        <AssignmentPageView></AssignmentPageView>
    </div>
    </>
  )
}

export default AssignmentPage
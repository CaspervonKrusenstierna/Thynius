import React from 'react'
import "../../../shared/styles/DashboardContainer.css"
import ResolveAssignmentPageContent from './hooks/ResolveAssignmentPageContent';
import DashboardContainerSideBar from '../../../shared/components/dashboardcontainersidebar/DashboardContainerSideBar';
import { useLocation, useParams } from 'react-router-dom';
import useAssignmentTabs from './hooks/useAssignmentTabs';
import useExtendedAssignmentInfo from './hooks/useExtendedAssignmentInfo';

const AssignmentPageView = () => {
  const {id} = useParams();
  const pageContent = ResolveAssignmentPageContent();
  const location = useLocation();
  const assignmentTabs = useAssignmentTabs(id, location.state);
  const extendedAssignmentInfo = useExtendedAssignmentInfo(id);

  return (
    <div className='DashboardContainer-Container'>
        <div className='DashboardContainer'>
        <DashboardContainerSideBar sidebartitle={location.state?.assignmentName} navigationtitle="Information" tabs={assignmentTabs}></DashboardContainerSideBar>
        <div className='DashboardContainer-Content'>
          {pageContent}
        </div>
        </div>
    </div>
  )
}

export default AssignmentPageView
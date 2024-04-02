import React from 'react'
import "../../../shared/styles/DashboardContainer.css"
import ResolveAssignmentPageContent from './hooks/ResolveAssignmentPageContent';
import { useLocation, useParams } from 'react-router-dom';
import useAssignmentTabs from './hooks/useAssignmentTabs';
import useExtendedAssignmentInfo from './hooks/useExtendedAssignmentInfo';
import { DashboardContainerSidebar } from '../../../shared/components/dashboard';

const AssignmentPageView = () => {
  const {id} = useParams();
  const pageContent = ResolveAssignmentPageContent();
  const location = useLocation();
  const assignmentTabs = useAssignmentTabs(id, location.state);
  const extendedAssignmentInfo = useExtendedAssignmentInfo(id);

  return (
    <div className='DashboardContainer-Container'>
        <div className='DashboardContainer'>
        <DashboardContainerSidebar sidebartitle={extendedAssignmentInfo?.Name} navigationtitle="Information" tabs={assignmentTabs}></DashboardContainerSidebar>
        <div className='DashboardContainer-Content'>
          {pageContent}
        </div>
        </div>
    </div>
  )
}

export default AssignmentPageView
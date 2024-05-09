import React, { createContext, useMemo, useState } from 'react'
import { useParams } from 'react-router-dom'
import useExtendedGroupInfo from './hooks/useExtendedGroupInfo'
import "../../../shared/styles/DashboardContainer.css"
import ResolveGroupContent from './hooks/ResolveGroupContent'
import useGroupTabs from './hooks/useGroupTabs'
import { DashboardContainerSidebar } from '../../../shared/components/dashboard'

export const groupinfoContext = createContext();

const GroupPageView = () => {
  let { id } = useParams();
  const [groupInfoTouched, setGroupInfoTouched] = useState(false);
  const groupInfo = useExtendedGroupInfo(id, groupInfoTouched);
  const pageContent = ResolveGroupContent(groupInfoTouched, setGroupInfoTouched);
  const GroupTabs = useGroupTabs(id);
  return (
    <groupinfoContext.Provider value={groupInfo}>
      <div className='DashboardContainer-Container'>
        <div className='DashboardContainer'>
          <DashboardContainerSidebar sidebartitle={groupInfo?.Name} navigationtitle="Information" tabs={GroupTabs}></DashboardContainerSidebar>
          <div className='DashboardContainer-Content'>
            {pageContent}
          </div>
        </div>
      </div>
    </groupinfoContext.Provider>
  )
}

export default GroupPageView
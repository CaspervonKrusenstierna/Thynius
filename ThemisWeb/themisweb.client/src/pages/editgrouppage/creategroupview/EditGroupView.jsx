import React, { createContext, useEffect, useRef } from 'react'
import "../../../shared/styles/DashboardContainer.css"
import { editGroup } from '../../../shared/network/group'
import { DashboardContainerSidebar } from '../../../shared/components/dashboard'
import BottomRightContainerButton from '../../../shared/components/dashboard/bottomrightcontainerbutton/BottomRightContainerButton'
import useManageGroupTabs from '../../../shared/hooks/useManageGroupTabs'
import useResolveCreateGroupContent from '../../../shared/hooks/useResolveManageGroupContent'
import { useParams } from 'react-router-dom'
import useEditGroupData from './useEditGroupData'


const EditGroupView = (props) => {
  const { id } = useParams();
  const [initialData, loading] = useEditGroupData(id);
  const tabs = useManageGroupTabs(false, id);
  const groupData = useRef(initialData);
  const pageContent = useResolveCreateGroupContent(groupData);

  function onEditGroupClick(){
    let data = groupData.current;
    editGroup(id, data.groupName, data.groupMembers, data.groupImg);
  }

  useEffect(() => {
    groupData.current = initialData;
  }, [loading])

  return (
    <>
    {loading ? <></> : 
    <div className='DashboardContainer-Container'>
      <div className='DashboardContainer'>
        <DashboardContainerSidebar state={{name: initialData.groupName, img: initialData.groupImg, members: initialData.groupMembers}} sidebartitle="Ändra grupp" navigationtitle="Sektioner" tabs={tabs}></DashboardContainerSidebar>
        <div className='DashboardContainer-Content'>
          {pageContent}
        </div>
        <BottomRightContainerButton onClick={onEditGroupClick}></BottomRightContainerButton>
      </div>
  </div>
  
  }</>
  )
}

export default EditGroupView
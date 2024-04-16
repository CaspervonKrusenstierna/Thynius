import React, { useEffect, useRef, useState } from 'react'
import "../../../shared/styles/DashboardContainer.css"
import { editGroup } from '../../../shared/network/group'
import { DashboardContainerSidebar } from '../../../shared/components/dashboard'
import BottomRightContainerButton from '../../../shared/components/dashboard/bottomrightcontainerbutton/BottomRightContainerButton'
import useManageGroupTabs from '../../../shared/hooks/useManageGroupTabs'
import useResolveCreateGroupContent from '../../../shared/hooks/useResolveManageGroupContent'
import { useNavigate, useParams } from 'react-router-dom'
import useEditGroupData from './useEditGroupData'


const EditGroupView = (props) => {
  const { id } = useParams();
  const [initialData, loading] = useEditGroupData(id);
  const tabs = useManageGroupTabs(false, id);
  const groupData = useRef(initialData);
  const pageContent = useResolveCreateGroupContent(groupData);
  const [forceRerender, setForceRerender] = useState(0);
  const navigate = useNavigate();

  async function onEditGroupClick(){
    groupData.current.nameError = false;
    groupData.current.pictureError = false;

    if(groupData.current.groupName == ""){
      groupData.current.nameError = true;
    }
    if(groupData.current.groupImg == null){
      groupData.current.pictureError = true;
    }

    if(groupData.current.nameError || groupData.current.pictureError){
      setForceRerender(forceRerender+1);
      navigate("/dashboard/editgroup/" + id + "/general", {state: {groupMembers: groupData.current.members, currentMembersPage: groupData.current.currentMembersPage, groupName: groupData.current.groupName, groupImg: groupData.current.groupName, nameError: groupData.current.nameError, pictureError: groupData.current.pictureError} })
      return;
    }

    let data = groupData.current;
    const response = await editGroup(id, data.groupName, data.groupMembers, data.groupImg);
    if(response.ok){
      navigate("/dashboard/groups")
    }
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
import React, { useEffect, useRef, useState } from 'react'
import "../../../shared/styles/DashboardContainer.css"
import { editGroup } from '../../../shared/network/group'
import { DashboardContainerSidebar } from '../../../shared/components/dashboard'
import BottomRightContainerButton from '../../../shared/components/dashboard/bottomrightcontainerbutton/BottomRightContainerButton'
import useManageGroupTabs from '../../../shared/hooks/useManageGroupTabs'
import useResolveCreateGroupContent from '../../../shared/hooks/useResolveManageGroupContent'
import { useNavigate, useParams } from 'react-router-dom'
import useEditGroupData from './useEditGroupData'
import { useTranslation } from 'react-i18next'


const EditGroupView = (props) => {
  const { id } = useParams();
  const [initialData, loading] = useEditGroupData(id);
  const tabs = useManageGroupTabs(false, id);
  const groupData = useRef(initialData);
  const pageContent = useResolveCreateGroupContent(groupData);
  const [forceRerender, setForceRerender] = useState(0);
  const navigate = useNavigate();
  const [t, i18n] = useTranslation();

  async function onEditGroupClick(){
    groupData.current.nameError = "";
    groupData.current.pictureError = false;

    if(groupData.current.groupName == ""){
      groupData.current.nameError = t("You need to submit a name for your group.");
    }
    if(groupData.current.groupName.length < 6){
      groupData.current.nameError = t("Your group name needs to contain at least 6 characters.");
    }
    if(groupData.current.groupName.length > 37){
      groupData.current.nameError = t("Your group name cannot contain more than 37 characters.");
    }
    if(groupData.current.groupImg == null){
      groupData.current.pictureError = t("You need to submit an image for your group.");
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
        <DashboardContainerSidebar state={{name: initialData.groupName, img: initialData.groupImg, members: initialData.groupMembers}} sidebartitle={t("Edit group")} navigationtitle={t("Sections")} tabs={tabs}></DashboardContainerSidebar>
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
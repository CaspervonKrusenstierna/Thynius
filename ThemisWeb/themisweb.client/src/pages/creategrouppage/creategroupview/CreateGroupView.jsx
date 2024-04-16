import React, {useRef, useState} from 'react'
import "../../../shared/styles/DashboardContainer.css"
import { createGroup, editGroup } from '../../../shared/network/group'
import { DashboardContainerSidebar } from '../../../shared/components/dashboard'
import BottomRightContainerButton from '../../../shared/components/dashboard/bottomrightcontainerbutton/BottomRightContainerButton'
import useManageGroupTabs from '../../../shared/hooks/useManageGroupTabs'
import useResolveCreateGroupContent from '../../../shared/hooks/useResolveManageGroupContent'
import { useNavigate, useParams } from 'react-router-dom'


const CreateGroupView = (props) => {
  const { id } = useParams();
  const navigate = useNavigate();
  const tabs = useManageGroupTabs(true, id);
  const groupData = useRef({groupMembers: [], currentMembersPage: 1, groupName: "", groupImg: null, nameError: false, pictureError: false});
  const [forceRerender, setForceRerender] = useState(0);
  const pageContent = useResolveCreateGroupContent(groupData);

  async function onCreateGroupClick(){
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
      navigate("/dashboard/creategroup/general", { state: groupData.current })
      return;
    }
    let data = groupData.current;
    let response = await createGroup(data.groupName, data.groupMembers, data.groupImg);
    if(response.ok){
      navigate("/dashboard/groups");
    }
  }
  
  return (
      <div className='DashboardContainer-Container'>
        <div className='DashboardContainer'>
          <DashboardContainerSidebar sidebartitle="Skapa grupp" navigationtitle="Sektioner" tabs={tabs}></DashboardContainerSidebar>
          <div className='DashboardContainer-Content'>
            {pageContent}
          </div>
          <BottomRightContainerButton onClick={onCreateGroupClick}></BottomRightContainerButton>
        </div>
      </div>
  )
}

export default CreateGroupView
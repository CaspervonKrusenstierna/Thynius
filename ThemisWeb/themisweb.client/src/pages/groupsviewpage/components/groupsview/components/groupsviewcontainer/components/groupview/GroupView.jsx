import React, { useContext} from 'react'
import "./GroupView.css"
import { Link } from 'react-router-dom'
import { sessionInfoContext } from '../../../../../../../../App'
import RowedItemOptions from '../../../../../../../../shared/components/dashboard/roweditemoptions/RowedItemOptions'
import {useNavigate} from "react-router-dom"
import { deleteGroup } from '../../../../../../../../shared/network/group'
import { Skeleton } from '@nextui-org/skeleton'

const GroupView = (props) => {
  const navigate = useNavigate();
  async function onDeleteClick(){
    let response = await deleteGroup(props.groupId)
    if(response.ok){
      let newGroupsInfo = [];
      for(let i = 0; props.groupsInfo.length > i; i++){
        if(props.groupsInfo[i].Id != props.groupId){
          newGroupsInfo.push(props.groupsInfo[i]);
        }
      }
      props.setGroupsInfo(newGroupsInfo);
    }
  }
  const optionsItems = [{text:"Ändra", onSelect: () => {navigate("/dashboard/editgroup/" + props.groupId + "/general", {state:{name:props.name,img:props.groupImg}})}}
  , {type: "DialogTrigger", text:"Ta Bort", onSelect: () => {onDeleteClick()}}]

  const sessionInfo = useContext(sessionInfoContext).sessionInfo;
  return (
        <div className='flex rounded w-[320px] h-[275px] md:w-[275px] md:h-[250px] mt-[15px] shadow-light'>
          {sessionInfo?.ID == props.managerId ? <RowedItemOptions title="Ändra" items={optionsItems} className="absolute ml-[250px] md:ml-[210px]"></RowedItemOptions> : <></>}
          <Link className='GroupView-Link' to={"/dashboard/group/" + props.groupId + "/Home"}>
            <img className="size-[185px] md:size-[150px] rounded mt-[45px] md:mt-[25px]" src={props.img}></img>
            <p className="text-lg mt-2">{props.name}</p>
          </Link>
        </div>
  )
}

export default GroupView
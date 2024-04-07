import React, { useContext } from 'react'
import "./GroupView.css"
import { Link } from 'react-router-dom'
import { sessionInfoContext } from '../../../../../../../../App'
import RowedItemOptions from '../../../../../../../../shared/components/dashboard/roweditemoptions/RowedItemOptions'
import {useNavigate} from "react-router-dom"
import { deleteGroup } from '../../../../../../../../shared/network/group'


const GroupView = (props) => {
  const navigate = useNavigate();
  
  const optionsItems = [{text:"Ändra", onSelect: () => {navigate("/dashboard/editgroup/" + props.groupId + "/general", {state:{name:props.name,img:props.groupImg}})}}
  , {type: "DialogTrigger", text:"Ta Bort", onSelect: () => {deleteGroup(props.groupId)}}]

  const sessionInfo = useContext(sessionInfoContext).sessionInfo;
  return (
    <div className='flex flex-column rounded w-[320px] h-[275px] md:w-[275px] md:h-[250px] mt-[15px] shadow-light'>
      {sessionInfo?.ID == props.managerId ? <RowedItemOptions title="Ändra" items={optionsItems} className="absolute ml-[250px] md:ml-[210px]"></RowedItemOptions> : <></>}
      <Link className='GroupView-Link' to={"/dashboard/group/" + props.groupId + "/Home"}>
        <img className="size-[185px] md:size-[150px] rounded mt-[45px] md:mt-[25px]" src={props.img}></img>
        <p className="text-lg mt-2">{props.name}</p>
      </Link>
    </div>

  )
}

export default GroupView
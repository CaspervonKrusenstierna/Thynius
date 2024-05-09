import React, { useContext } from 'react'
import "./GroupView.css"
import { Link } from 'react-router-dom'
import { sessionInfoContext } from '../../../../../../../../App'
import RowedItemOptions from '../../../../../../../../shared/components/dashboard/roweditemoptions/RowedItemOptions'
import {useNavigate} from "react-router-dom"
import { deleteGroup } from '../../../../../../../../shared/network/group'
import { useTranslation } from 'react-i18next'
import LoadingImage from '../../../../../../../../shared/components/dashboard/loadingimage/LoadingImage'

const GroupView = (props) => {
  const navigate = useNavigate();
  const [t, i18n] = useTranslation();

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
  const optionsItems = [{text: t("Edit"), onSelect: () => {navigate("/dashboard/editgroup/" + props.groupId + "/general", {state:{name:props.name,img:props.img}})}}
  , {type: "DialogTrigger", text: t("Delete"), onSelect: () => {onDeleteClick()}}]

  const sessionInfo = useContext(sessionInfoContext).sessionInfo;
  return (
        <div className='flex rounded w-[320px] h-[275px] md:w-[275px] md:h-[250px] mt-[15px] shadow-light'>
          {sessionInfo?.ID == props.managerId ? <RowedItemOptions title={t("Options")} items={optionsItems} className={i18n.language == "sv-SE" ? "absolute ml-[250px] md:ml-[210px]" : "absolute ml-[238px] md:ml-[198px]"}></RowedItemOptions> : <></>}
          <Link className='GroupView-Link' to={"/dashboard/group/" + props.groupId + "/Home"}>
            <LoadingImage className="size-[185px] md:size-[150px] rounded mt-[45px] md:mt-[25px]" img={props.img}></LoadingImage>
            <p className={props.name.length > 26 ? "text-[16px] mt-[14px] w-[80%] h-10 break-words break-all" : "text-[20px] mt-[14px]"}>{props.name}</p>
          </Link>
        </div>
  )
}

export default GroupView
import React, { useContext } from 'react'
import { Link } from 'react-router-dom'
import "./AssignmentView.css"
import { groupinfoContext } from '../../../../GroupPageView'
import { deleteAssignment } from '../../../../../../../shared/network/assignment'
import { sessionInfoContext } from '../../../../../../../App'
import RowedItemOptions from '../../../../../../../shared/components/dashboard/roweditemoptions/RowedItemOptions'
import {useNavigate, useParams} from "react-router-dom"

const AssignmentView = (props) => {
  const groupInfo = useContext(groupinfoContext);
  const sessionInfo = useContext(sessionInfoContext).sessionInfo;
  const navigate = useNavigate();
  let { id } = useParams();

  const optionsItems = [{text:"Ändra", onSelect: () => {navigate(`/dashboard/group/${id}/EditAssignment/${props.assignmentId}`)}}
  , {type: "DialogTrigger", text:"Ta Bort", onSelect: () => {deleteAssignment(props.assignmentId)}}]

  return (
  <div className='relative flex flex-column rounded w-[280px] h-[260px] md:w-[225px] md:h-[200px] mt-[15px] shadow-light'>
    {groupInfo?.ManagerData?.Id == sessionInfo?.ID ? <RowedItemOptions title="Ändra" items={optionsItems} className="absolute ml-[200px] md:ml-[160px]"></RowedItemOptions> : <></>}
    <Link className='AssignmentView-Link' state={{userDatas: groupInfo.userDatas, assignmentName: props.assignmentName}} to={`/dashboard/Assignment/${props.assignmentId}/general`}>
      <img className='size-[170px] md:size-[115px] rounded mt-[20px] md:mt-[25px]' src={props.img}></img>
      <p className='text-lg mt-2'>{props.assignmentName}</p>
    </Link>
  </div>


  )
}

export default AssignmentView
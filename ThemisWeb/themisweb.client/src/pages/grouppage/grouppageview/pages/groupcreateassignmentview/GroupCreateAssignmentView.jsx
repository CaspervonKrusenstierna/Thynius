import React, { useContext, useRef, useState } from 'react'
import "./GroupCreateAssignmentView.css"
import CreateAssignmentSubmitButton from './components/createassignmentsubmitbutton/CreateAssignmentSubmitButton';
import CreateAssignmentNameInput from './components/createassignmentsnameinput/CreateAssignmentNameInput';
import { Calendar } from "@/components/ui/calendar"
import { groupinfoContext } from '../../GroupPageView';
import useFetch from '../../../../../shared/hooks/useFetch';

const GroupCreateAssignmentView = (props) => {
  const groupInfo = useContext(groupinfoContext);
  const [selectedDate, setSelectedDate] = useState();
  const assignmentName = useRef("");
  function onCreateAssignmentClick(){
    useFetch("/assignment?groupId=" + groupInfo.id + "&AssignmentName="+assignmentName.current+"&dueDate="+selectedDate, "POST")
  }
  return (
    <div className='GroupCreateAssignmentView'>
        <div className='GroupCreateAssignmentView-Top'>
            <CreateAssignmentNameInput Title="Namn" onChange={(s) => {assignmentName.current = s.target.value}}></CreateAssignmentNameInput>
            <div className='CreateAssignment-DaypickerContainer'>
                <p className='CreateAssignment-DaypickerContainer-Text'>Sista inlämningsdatum</p>
                <Calendar onSelect={setSelectedDate} selected={selectedDate} className="rounded-md border width-[100%]" mode="single"></Calendar>
            </div>
        </div>
        <div className='GroupCreateAssignmentView-Bottom'>
            <CreateAssignmentSubmitButton onClick={onCreateAssignmentClick}></CreateAssignmentSubmitButton>
        </div>
    </div>
  )
}

export default GroupCreateAssignmentView
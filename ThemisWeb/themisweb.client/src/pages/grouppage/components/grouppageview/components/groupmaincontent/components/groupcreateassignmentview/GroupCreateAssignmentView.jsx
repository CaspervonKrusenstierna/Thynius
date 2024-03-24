import React, { useRef, useState } from 'react'
import { DayPicker } from 'react-day-picker';
import { format } from 'date-fns';
import 'react-day-picker/dist/style.css';
import "./GroupCreateAssignmentView.css"
import VerticalInput from '../../../../../../../../shared/components/verticalinput/VerticalInput';
import CreateAssignmentSubmitButton from './createassignmentsubmitbutton/CreateAssignmentSubmitButton';
import useFetch from '../../../../../../../../shared/hooks/useFetch';

const GroupCreateAssignmentView = (props) => {
  const [selectedDate, setSelectedDate] = useState();
  const assignmentName = useRef("");
  function onCreateAssignmentClick(){
    useFetch("/assignment?groupId=" 
    + props.groupInfo.Id + "&AssignmentName="+assignmentName.current+"&dueDate="+selectedDate, "POST")
  }
  return (
    <div className='GroupCreateAssignmentView'>
        <div className='GroupCreateAssignmentView-Top'>
            <VerticalInput Title="Namn" onChange={(s) => {assignmentName.current = s.target.value}}></VerticalInput>
            <div className='CreateAssignment-DaypickerContainer'>
                <p className='CreateAssignment-DaypickerContainer-Text'>Sista inlämningsdatum</p>
                <DayPicker modifiersClassNames={{selected: 'my-selected', today: 'my-today'}} mode="single" selected={selectedDate}  onSelect={setSelectedDate}></DayPicker>
            </div>
        </div>
        <div className='GroupCreateAssignmentView-Bottom'>
            <CreateAssignmentSubmitButton onClick={onCreateAssignmentClick}></CreateAssignmentSubmitButton>
        </div>
    </div>
  )
}

export default GroupCreateAssignmentView
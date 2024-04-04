import React, { useContext, useRef, useState } from 'react'
import "./GroupCreateAssignmentView.css"
import { groupinfoContext } from '../../GroupPageView';
import useFetch from '../../../../../shared/hooks/useFetch';
import { DatePicker } from './datepicker/DatePicker';
import TimePicker from './timepicker/TimePicker';
import { Input } from '@nextui-org/input';
import {Textarea} from "@nextui-org/input";

const GroupCreateAssignmentView = (props) => {
  const groupInfo = useContext(groupinfoContext);
  const [selectedDate, setSelectedDate] = useState();
  const assignmentName = useRef("");


  function onCreateAssignmentClick(){
    useFetch("/assignment?groupId=" + groupInfo.id + "&AssignmentName="+assignmentName.current+"&dueDate="+selectedDate, "POST")
  }
  return (
    <div className='GroupCreateAssignmentView'>
      <div className='flex flex-col gap-5'>
        <div className='flex flex-col gap-3 md:flex-row'>
          <Input className='w-full sm:w-[280px] text-base' variant="faded" radius='sm' type='name' label="Uppgiftsnamn"></Input>
          <DatePicker></DatePicker>
        </div>
        <Textarea variant="faded" label="Beskrivning" placeholder="Beskrivning av uppgiften" className='w-full sm:w-[400px]'></Textarea>
        <div className='h-full'></div>
      </div>

    </div>
  )
}

export default GroupCreateAssignmentView
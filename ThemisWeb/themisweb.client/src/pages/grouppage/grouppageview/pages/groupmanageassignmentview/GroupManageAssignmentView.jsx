import React, { useEffect, useRef, useState } from 'react'
import "./GroupManageAssignmentView.css"
import { DatePicker } from './datepicker/DatePicker';
import { Input } from '@nextui-org/input';
import {Textarea} from "@nextui-org/input";
import { Button } from "@/components/ui/button"

const GroupManageAssignmentView = (props) => {
  const [state, setState] = useState(props.initialState ? props.initialState : {date: null, image: null});
  const ref = useRef(props.initialRef ? props.initialRef : {name: null, description: null});
  return (
    <div className='GroupManageAssignmentView'>
      <div className='flex flex-col gap-5 h-full'>
        <div className='flex flex-col gap-3 md:flex-row'>
          <Input className='w-full sm:w-[280px] text-base' defaultValue={ref.current.name} onChange={s => {ref.current.name = s.target.value}} variant="faded" radius='sm' type='name' label="Uppgiftsnamn"></Input>
          <DatePicker date={state.date} setDate={(date) => {/*we have to do this to make react rerender:*/let temp = {}; temp.image=state.image; temp.date = date; setState(temp)}}></DatePicker> 
        </div>
        <Textarea variant="faded" defaultValue={ref.current.description} onChange={s => {ref.current.description = s.target.value}} label="Beskrivning" placeholder="Beskrivning av uppgiften" className='w-full sm:w-[400px]'></Textarea>
        <div>
          <input type="file"  accept=".jpg,.img" className="px-3" onChange={(e) => {let temp = {}; temp.date = state.date; temp.image = URL.createObjectURL(e.target.files[0]); setState(temp)}}></input>
          {state.image ? <img src={state.image} className='ml-3 mt-5 w-[100px] h-[100px] sm:w-[130px] sm:h-[130px]'></img> : <div className='w-[100px] h-[100px] sm:w-[130px] sm:h-[130px]'></div>}
        </div>
         <div className='h-[100%] w-full flex flex-row justify-end items-end'>
          <Button onClick={() => {props.onSubmit(ref.current, state)}} className="mr-1 mb-1" size="default">
            <p className='text-lg'>Bekräfta</p>
          </Button>
         </div>
      </div>
    </div>
  )
}

export default GroupManageAssignmentView
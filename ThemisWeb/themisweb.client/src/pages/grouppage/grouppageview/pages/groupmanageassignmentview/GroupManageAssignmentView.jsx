import React, { useState } from 'react'
import { DatePicker } from './datepicker/DatePicker';
import { Input } from '@nextui-org/input';
import {Textarea} from "@nextui-org/input";
import { Button } from "@/components/ui/button"
import useManageAssignmentErrors from './useManageAssignmentErrors';

const GroupManageAssignmentView = (props) => {
  const [state, setState] = useState(props.initialState ? props.initialState : {date: "", image: "", name: "", description: ""});
  const errors = useManageAssignmentErrors(state);

  function updateName(name){
    let temp = {};
    temp.date = state.date; 
    temp.description = state.description;
    temp.image = state.image; 
    temp.name = name.target.value;
    setState(temp)
  }
  function updateDescription(desc){
    let temp = {};
    temp.date = state.date; 
    temp.image = state.image; 
    temp.name = state.name;
    temp.description = desc.target.value;
    setState(temp)
  }
  function updateImage(image){
     let temp = {};
     temp.date = state.date; 
     temp.description = state.description;
     temp.name = state.name;
     temp.image = URL.createObjectURL(image.target.files[0]); 
     setState(temp)
  }
  function updateDate(date){
    let temp = {}; 
    temp.image = state.image;
    temp.description = state.description;
    temp.name = state.name;
    temp.date = date; 
    setState(temp);
  }

  return (
    <div className='flex flex-col gap-5 h-[100%] w-[100%] mt-[70px] md:mt-[96px] items-between'>
      <div className='flex flex-col gap-3 md:flex-row w-[80%] justify-between'>
        <div>
          {props.hasClickedSubmit && errors?.nameError ? <p className='text-red-600 h-[50px]  md:h-[24px]'>{errors.nameError}</p> : <div className=' h-[50px] md:h-[24px]'/>}
          <Input className='w-full sm:w-[280px] text-base' defaultValue={state.name} onChange={updateName} variant="faded" radius='sm' type='name' label="Uppgiftsnamn"></Input>
        </div>
        <div>
          {props.hasClickedSubmit && errors?.dateError ? <p className='text-red-600 h-[50px] md:h-[24px]'>{errors.dateError}</p> : <div className=' h-[50px] md:h-[24px]'/>}
          <DatePicker date={state.date} setDate={updateDate}></DatePicker> 
        </div>
        
      </div>
      <div>
        {props.hasClickedSubmit && errors?.descriptionError ? <p className='text-red-600 h-[50px] md:h-[24px]'>{errors.descriptionError}</p> : <div className=' h-[50px] md:h-[24px]'/>}
        <Textarea variant="faded" defaultValue={state.description} onChange={updateDescription} label="Beskrivning" placeholder="Beskrivning av uppgiften" className='w-full sm:w-[400px]'></Textarea>
      </div>
      <div>
      <div className="px-3">
        <p className={props.hasClickedSubmit && errors?.imageError ? "text-red-600 text-xl pb-3" : "text-xl pb-3"}>Gruppbild</p>
        <input type="file"  accept=".jpg,.img" onChange={updateImage}></input>
        {state.image ? <img src={state.image} className='ml-3 mt-5 w-[100px] h-[100px] sm:w-[130px] sm:h-[130px]'></img> : <div className='w-[100px] h-[100px] sm:w-[130px] sm:h-[130px]'></div>}
      </div>
      </div>
        <div className='h-[100%] w-full flex flex-row justify-end items-end'>
        <Button onClick={() => {props.onSubmit(state)}} className="mr-1 mb-1" size="default">
          <p className='text-lg'>Bekräfta</p>
        </Button>
        </div>
    </div>
)
}

export default GroupManageAssignmentView
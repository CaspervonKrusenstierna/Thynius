import React from 'react'
import "./ChooseableAssignment.css"
import { ImageAvatar } from '../../../../../shared/assets'
import {
  AlertDialog,
  AlertDialogAction,
  AlertDialogCancel,
  AlertDialogContent,
  AlertDialogDescription,
  AlertDialogFooter,
  AlertDialogHeader,
  AlertDialogTitle,
  AlertDialogTrigger,
} from "@/components/ui/alert-dialog"
import { Button } from "@/components/ui/button"

const ChooseableAssignment = (props) => {
  return (
    <AlertDialog>
    <AlertDialogTrigger asChild>
      <button className='ChooseableAssignment'>
        <img className='ChooseableAssignment-Img' src={ImageAvatar}></img>
        <p className='ChooseableAssignment-Text'>{props.name}</p>
      </button>
    </AlertDialogTrigger>
    <AlertDialogContent>
      <AlertDialogHeader>
        <AlertDialogTitle>Är du säker?</AlertDialogTitle>
        <AlertDialogDescription>
          När du har skickat in din text så kan du inte ångra det.
        </AlertDialogDescription>
      </AlertDialogHeader>
      <AlertDialogFooter className="gap-2">
        <AlertDialogCancel>Ångra</AlertDialogCancel>
        <AlertDialogAction onClick={() => {props.onClick(props.Id)}} >Skicka in</AlertDialogAction>
      </AlertDialogFooter>
    </AlertDialogContent>
  </AlertDialog>
  )
}

export default ChooseableAssignment
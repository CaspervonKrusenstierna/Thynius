import React from 'react'
import "./ChooseableAssignment.css"
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
import { ImageAvatar } from '../../../../../shared/assets'
import { useTranslation } from 'react-i18next'

const ChooseableAssignment = (props) => {
  const [t, i18n] = useTranslation();
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
        <AlertDialogTitle>{t("Are you sure?")}</AlertDialogTitle>
        <AlertDialogDescription>
          {t("Once you have submitted your text you cannot undo it.")}
        </AlertDialogDescription>
      </AlertDialogHeader>
      <AlertDialogFooter className="gap-2">
        <AlertDialogCancel className="outline-none border-none">{t("Cancel")}</AlertDialogCancel>
        <AlertDialogAction onClick={() => {props.onClick(props.Id)}} >{t("Send in")}</AlertDialogAction>
      </AlertDialogFooter>
    </AlertDialogContent>
  </AlertDialog>
  )
}

export default ChooseableAssignment
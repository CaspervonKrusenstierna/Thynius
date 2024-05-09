import React from 'react'
import { Input } from "@/components/ui/input"
import { useTranslation } from 'react-i18next'
const SubmittmentsSearch = (props) => {
  const [t, i18n] = useTranslation();
  return (
    <Input placeholder={t("Search")} onChange={(e) => {props.setSearch(e.target.value)}}></Input>
  )
}

export default SubmittmentsSearch
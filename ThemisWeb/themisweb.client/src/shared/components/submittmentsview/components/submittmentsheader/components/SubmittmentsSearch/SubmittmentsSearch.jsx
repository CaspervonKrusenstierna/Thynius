import React from 'react'
import { Input } from "@/components/ui/input"
const SubmittmentsSearch = (props) => {
  return (
    <Input placeholder="Sök" onChange={(e) => {props.setSearch(e.target.value)}}></Input>
  )
}

export default SubmittmentsSearch
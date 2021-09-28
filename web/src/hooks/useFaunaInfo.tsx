import { createContext, ReactNode, useContext, useEffect, useState } from "react"
import { faunaClient } from '../services/fauna'
import { query as q } from 'faunadb'
import { formatToBRL } from "../services/formatter"

interface FaunaInfoProviderProps {
  children?: ReactNode;
}

interface FaunaResponse {
  data: FaunaData,
}

type FaunaTransaction = {
  amount: number,
  category: string,
  title: string,
  type: string
}

type FaunaData = {
  name: string,
  id: string,
  wallet: number,
  income: number,
  outcome: number,
  transactionsList: FaunaTransaction[],
}

type FormattedFaunaData = {
  name: string,
  id: string,
  wallet: string,
  income: string,
  outcome: string,
  transactionsList: FaunaTransaction[],
}

interface FaunaInfoContext {
  userInfo: FaunaData,
  formattedUserInfo: FormattedFaunaData,
  getUserInfo: (id: string) => void
}

const FaunaInfoContext = createContext({} as FaunaInfoContext)

export function FaunaInfoProvider({ children }: FaunaInfoProviderProps) {
  const [userInfo, setUserInfo] = useState({} as FaunaData)
  const [formattedUserInfo, setFormattedUserInfo] = useState({} as FormattedFaunaData)

  function getUserInfo(id: string) {
    try {
      faunaClient.query(q.Get(q.Match(q.Index('user_by_id'), id))).then((res) => {
        const { data } = res as FaunaResponse
        setUserInfo(data)
        setFormattedUserInfo({
          name: data.name,
          id: data.id,
          wallet: formatToBRL(data.wallet),
          income: formatToBRL(data.income),
          outcome: formatToBRL(data.outcome),
          transactionsList: data.transactionsList
        })
      })
    } catch (error) {
    }
  }

  return (
    <FaunaInfoContext.Provider value={{ userInfo, formattedUserInfo, getUserInfo }}>
      {children}
    </FaunaInfoContext.Provider>
  )
}

export function useFaunaInfo(id: string) {
  const faunaInfo = useContext(FaunaInfoContext);
  faunaInfo.getUserInfo(id)
  return faunaInfo;
}
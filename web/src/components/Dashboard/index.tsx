import { Block, DashboardContainer } from "./styles"
import { FiArrowUpCircle, FiArrowDownCircle } from 'react-icons/fi'
import { useFaunaInfo } from "../../hooks/useFaunaInfo"

interface DashboardProps {
  userId: string
}

export function Dashboard({ userId }: DashboardProps) {
  const { formattedUserInfo } = useFaunaInfo(userId);

  return (
    <DashboardContainer>
      <Block>
        <p>
          Entradas
          <FiArrowDownCircle color="#33CC95" />
        </p>
        <p>{formattedUserInfo.income}</p>
      </Block>
      <Block>
        <p>
          Saídas
          <FiArrowUpCircle color="#E52E4D" />
        </p>
        <p>{formattedUserInfo.outcome}</p>
      </Block>
      <Block>
        <p>
          Total
          {/* eslint-disable-next-line @next/next/no-img-element */}
          <img src="/icon.svg" alt="dinheiro" />
        </p>
        <p>{formattedUserInfo.wallet}</p>
      </Block>
    </DashboardContainer>
  )
}
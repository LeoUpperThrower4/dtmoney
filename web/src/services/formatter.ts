export const formatToBRL = new Intl.NumberFormat([], {
  style: 'currency',
  currency: 'BRL'
}).format
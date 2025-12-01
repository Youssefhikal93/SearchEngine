// Minimal front-end script to call the WeatherForecast API
document.getElementById('load')?.addEventListener('click', async () => {
  const res = await fetch('/WeatherForecast');
  const data = await res.json();
  const list = document.getElementById('results');
  if (!list) return;
  list.innerHTML = '';
  data.forEach(item => {
    const li = document.createElement('li');
    li.textContent = `${item.date} - ${item.temperatureC}°C - ${item.summary}`;
    list.appendChild(li);
  });
});

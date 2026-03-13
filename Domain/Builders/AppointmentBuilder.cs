using System;
using scheduling_management.Domain.Entities;

namespace scheduling_management.Domain.Builders;

public class AppointmentBuilder
{
    private readonly Appointment _appointment;

    public AppointmentBuilder WithEstabilishmentId(Guid estabilishmentId)
    {
        _appointment.ProfessionalId = estabilishmentId;
        return this;
    }
    
    public AppointmentBuilder WithProfessionalId(Guid id)
    {
        _appointment.ProfessionalId = id;
        return this;
    }
    
    public AppointmentBuilder WithServicetId(Guid id)
    {
        _appointment.ServiceId = id;
        return this;
    }
    
    public AppointmentBuilder WithClientId(Guid id)
    {
        _appointment.ClientId = id;
        return this;
    }

    public AppointmentBuilder WithSchedulingDateOnly(DateOnly date)
    {
        _appointment.SchedulingDateOnly = date;
        return this;
    }

    public AppointmentBuilder WithStartHours(TimeOnly start)
    {
        _appointment.StartHours = start;
        return this;
    }

    public AppointmentBuilder WithEndHours(TimeOnly end)
    {
        _appointment.EndHours = end;
        return this;
    }
    
    public Appointment Build() =>  _appointment;
}
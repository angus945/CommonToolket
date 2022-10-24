include = {"Math", "Vector"}

function MoveUp (properity, delta)
    pos = properity.position;
    pos.y = pos.y + properity.Speed * delta;

    properity.position = pos;

    -- print(properity.position);
end

function MoveDown (properity, delta)
    pos = properity.position;
    pos.y = pos.y - properity.Speed * delta;

    properity.position = pos;
end
